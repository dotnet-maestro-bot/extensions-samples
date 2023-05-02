
# Kick off the binary in the background. [Prints stats every 5s]
Start-Process -NoNewWindow .\ResourceUtilization.exe

# Watch the perf counters, as well as the output.  [Prints stats every 20s]
$manualResetEvent = New-Object -TypeName System.Threading.ManualResetEvent -ArgumentList $false
while (-not $manualResetEvent.WaitOne(20000)) {
	Get-Counter -Counter "\COSMIC Containers(total)\% Memory Limit Utilization" -ErrorAction SilentlyContinue
	Get-Counter -Counter "\COSMIC Containers(total)\% CPU Limit Utilization" -ErrorAction SilentlyContinue
}